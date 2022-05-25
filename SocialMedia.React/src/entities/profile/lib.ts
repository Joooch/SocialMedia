import * as api from 'shared/api/apiRequest'
import { User } from 'shared/models'
export function fetchProfile(userId: string) {
    return api.get<User>("/api/profile/" + userId)
        .then(res => {
            return res.data
        })
}