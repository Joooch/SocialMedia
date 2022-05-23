import * as api from 'shared/api/apiRequest'
import { Post } from '../types';

export type pageDto = {
    pageSize: number;
    lastId?: string;
}

export type uploadedImageDto = {
    imageId: string;
}

export async function getFeed(page: pageDto) {
    let response = await api.get<Post[]>("/api/posts/getFeed", { params: page });
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