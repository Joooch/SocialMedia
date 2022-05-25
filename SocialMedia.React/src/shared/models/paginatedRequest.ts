export type Filter = {
    path: string;
    value: string;
}

export type PaginatedRequest = {
    offset?: Date;
    page?: number;
    pageSize: number;
    filters?: Filter[];

    sortKey?: string;
    sortDirection?: string;
}