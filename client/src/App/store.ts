import { combineReducers, configureStore } from "@reduxjs/toolkit";
import type { PreloadedState } from "@reduxjs/toolkit";
import authSlice from "./feature/auth/authSlice";
import profileSlice from "./feature/profile/profileSlice";
import { profileApi } from "../services/profileApi";
import { locationApi } from "../services/locationApi";
import { authApi } from "../services/authApi";
import userApi from "../services/userApi";

const rootReducer = combineReducers({
	auth: authSlice,
	profile: profileSlice,
	[authApi.reducerPath]: authApi.reducer,
	[profileApi.reducerPath]: profileApi.reducer,
	[locationApi.reducerPath]: locationApi.reducer,
	[userApi.reducerPath]: userApi.reducer,
});

export const setupStore = (preloadedState?: PreloadedState<RootState>) => {
	return configureStore({
		reducer: rootReducer,
		middleware: (getDefaultMiddleware) =>
			getDefaultMiddleware().concat(
				authApi.middleware,
				profileApi.middleware,
				locationApi.middleware,
				userApi.middleware
			),
		preloadedState,
	});
};

export type RootState = ReturnType<typeof rootReducer>;
export type AppStore = ReturnType<typeof setupStore>;
export type AppDispatch = AppStore["dispatch"];
