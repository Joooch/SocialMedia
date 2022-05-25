import * as api from 'shared/api/apiRequest'
import { PaginatedRequest, PaginatedResult, Post } from 'shared/models'

export type UploadedImageDto = {
    imageId: string;
}

export type Filter = {
    path: string,
    value: string
}

export async function getFeed(pageData: PaginatedRequest) {
    let response = await api.post<PaginatedResult<Post>>("/api/posts/getFeed", pageData);
    return response.data;
}

export async function createPost(content: string, imageList?: string[]) {
    let response = await api.post<Post>("/api/posts/create", { content, images: imageList });
    return response.data;
}

export async function uploadImage(file: File) {
    let formData = new FormData();
    formData.append("file", file);

    let response = await api.post<UploadedImageDto>('/api/posts/uploadImage', formData, {
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    });

    return response.data;
}