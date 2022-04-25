import axios from "axios";
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

export function getCookie(key: string) {
    var b = document.cookie.match("(^|;)\\s*" + key + "\\s*=\\s*([^;]+)");
    return b ? b.pop() : undefined;
}

export function setCookie(name: string, value: string, days: number = 7) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}