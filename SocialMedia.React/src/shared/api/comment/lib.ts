import * as api from 'shared/api/apiRequest'
import { PaginatedRequest, PaginatedResult, Comment } from 'shared/models'


export async function getFeed(postId: string, pageData: PaginatedRequest) {
    let response = await api.post<PaginatedResult<Comment>>("/api/comment/findInPost/" + postId, pageData);
    return response.data;
}

export async function createComment(content: string, postId: string) {
    let response = await api.post<Comment>("/api/comment/createComment/", { content, postId });
    return response.data;
}