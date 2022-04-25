import type { User } from "../types";
import * as api from '../apiRequest'

export type userResponseDto = {
    profile: User;
    email: string;
}

export async function getCurrentUser(): Promise<userResponseDto | undefined> {
    const response = await api.get<userResponseDto>("/api/user");
    return response.data;
}

export async function getTokenByGoogle(token: string): Promise<string> {
    const response = await api.post("/api/googlelogin", { token })
    return response.data.token
}

/* export async function sendLogoutCommand(){
    await api.post("/api/user/logout");
} */