import { User } from "./user";

export type Comment = {
    id: string;
    owner: User;
    content: string;
    createdAt: Date;
}