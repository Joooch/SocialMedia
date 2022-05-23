import { User } from 'shared/api'
import * as api from 'shared/api/apiRequest'
export function fetchProfile( userId: string ){
    return api.get<User>("/api/profile/" + userId)
        .then( res => {
            return res.data
        } )
}