import * as api from 'shared/api/apiRequest'

export type ProfileUpdateDto = {
    firstName: string;
    lastName: string;
    profileImage?: File;
    date: Date;
    address: string;
    region: string;
    city: string;
    country: string;
}

export function profileUpdate(dto: ProfileUpdateDto) {
    return api.put("/api/profile", dto)
}

export function profileUpdateImage( file: File ){
    var formData = new FormData();
    
    formData.append("file", file);
    return api.put('/api/profile/image', formData, {
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    })
}