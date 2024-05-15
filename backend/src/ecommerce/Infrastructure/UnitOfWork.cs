using System.Transactions;
using ecommerce.Application;
using ecommerce.Application.Common.Exceptions;
using ecommerce.Application.Repositories;
using ecommerce.Infrastructure.Data;
using ecommerce.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore.Storage;

namespace ecommerce.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextTransaction? _transaction;
        private bool _disposed;
        //
        private readonly ApplicationDbContext _context;

        // repositories

        public IUserRepository UserRepository { get; }
        //
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            // repositories

            UserRepository = new UserRepository(_context);
        }

        // save changes
        public int SaveChanges() => _context.SaveChanges();

        public async Task<int> SaveChangesAsync(CancellationToken token) => await _context.SaveChangesAsync(token);

        // transaction
        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        // commit
        public void Commit()
        {
            if (_transaction == null)
            {
                throw new UserFriendlyException(ErrorCode.Internal, "No transaction to commit");
            }
            try
            {
                _context.SaveChanges();
                _transaction.Commit();
            }
            finally
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public async Task CommitAsync(CancellationToken token)
        {
            if (_transaction == null)
            {
                throw new UserFriendlyException(ErrorCode.Internal, "No transaction to commit");
            }

            try
            {
                await _context.SaveChangesAsync(token);
                _transaction.Commit();
            }
            finally
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        // rollback
        public void Rollback()
        {
            if (_transaction == null)
            {
                throw new UserFriendlyException(ErrorCode.Internal, "No transaction to commit");
            }

            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;
        }

        public async Task RollbackAsync()
        {
            if (_transaction == null)
            {
                throw new UserFriendlyException(ErrorCode.Internal, "No transaction to commit");
            }

            await _transaction.RollbackAsync();
            _transaction.Dispose();
            _transaction = null;
        }

        // dispose
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transaction?.Dispose();
                    _context.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // execute transaction
        public async Task ExecuteTransactionAsync(Action action, CancellationToken token)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                action();
                await _context.SaveChangesAsync(token);
                await transaction.CommitAsync(token);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new UserFriendlyException(ErrorCode.Internal, "Can't execute transaction: " + ex);
            }
        }

        public async Task ExecuteTransactionAsync(Func<Task> action, CancellationToken token)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await action();
                await _context.SaveChangesAsync(token);
                await transaction.CommitAsync(token);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new UserFriendlyException(ErrorCode.Internal, "Can't execute transaction: " + ex);
            }
        }
    }
}
