import { User } from 'shared/api'
import * as api from 'shared/api/apiRequest'
export function fetchProfile( userId: string ){
    api.get<User>("/api/user/" + userId)
        .then( res => {
            console.log( res )
            console.log( res.data )
        } )
}