import * as api from 'shared/api/apiRequest'
import { PaginatedResult, Post } from '../types';

export type pagedRequestDto = {
    offset?: Date;
    page?: number;
    pageSize: number;
}

export type uploadedImageDto = {
    imageId: string;
}

export async function getFeed(pageData: pagedRequestDto) {
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

    let response = await api.post<uploadedImageDto>('/api/posts/uploadImage', formData, {
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    });

    return response.data;
}