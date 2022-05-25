import { User } from "./user";

export type Post = {
    id: string;
    createdAt: Date;
    content: string;
    userOwner: User;
    images: string[];
}