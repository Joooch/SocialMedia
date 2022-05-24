export type User = { // aka Profile
    userId: string;
    firstName: string;
    lastName: string;
}

export type UserFull = User & {
    address: string;
    city: string;
    region: string;
    country: string;
}

export type Post = {
    id: string;
    createdAt: Date;
    content: string;
    userOwner: User;
    images: string[];
}

export type PaginatedResult<T> = {
    offset?: Date;
    page?: number;
    pageSize: number;
    total: number;
    items: T[];
}