import { IProfile } from "../profile/profileSlice.types";

export interface IAuthSliceState {
	accessToken: string | null;
	isAuthenticated: boolean;
}

export type TSignUpRequest = {
	email: string;
	fullName: string;
	password: string;
};

export type TAuthResponseError = {
	status: number | string;
	data: {
		error: boolean;
		message: string;
	};
};

export type TSignInRequest = {
	email: string;
	password: string;
};

export type TDecodedJWT = IProfile;
