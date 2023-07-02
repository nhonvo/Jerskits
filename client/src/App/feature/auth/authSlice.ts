import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { IAuthSliceState } from "./authSlice.types";

const initialState: IAuthSliceState = {
  accessToken: null,
};

const authSlice = createSlice({
  name: "authSlice",
  initialState,
  reducers: {
    setToken: (state, { payload }: PayloadAction<{ accessToken: string }>) => {
      state.accessToken = payload.accessToken;
    },
    removeToken: (state) => {
      state.accessToken = null;
    },
  },
});
export const { setToken, removeToken } = authSlice.actions;
export default authSlice.reducer;
