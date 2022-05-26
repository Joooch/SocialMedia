import { UserFull } from 'shared/models';
import * as api from '../apiRequest'

export type UserResponseDto = {
    profile: UserFull;
    email: string;
}

export async function getCurrentUser(): Promise<UserResponseDto | undefined> {
    const response = await api.get<UserResponseDto>("/api/user");
    return response.data;
}

export async function getTokenByGoogle(token: string): Promise<string> {
    const response = await api.post("/api/googlelogin", { token })
    return response.data.token
}