import * as api from 'shared/api/apiRequest'
import { PaginatedRequest, PaginatedResult, User } from 'shared/models';
import { FriendStatus } from 'shared/models/friendStatus';


export async function sendFriendRequest(userId: string) {
    let response = await api.post("/api/friends/sendRequest", { userId });
    return response.data;
}

export async function getFriends(userId: string, pageInfo: PaginatedRequest) {
    let response = await api.post<PaginatedResult<User>>("/api/friends/getFriends", { userId, pageInfo });
    return response.data;
}

export async function getFriendStatus(userId: string) {
    let response = await api.post<FriendStatus>("/api/friends/getFriendStatus", { userId });
    return response.data;
}


type friendsCountDto = {
    count: number
}
export async function getFriendsCount(userId: string) {
    let response = await api.post<friendsCountDto>("/api/friends/getFriendsCount", { userId });
    return response.data.count;
}