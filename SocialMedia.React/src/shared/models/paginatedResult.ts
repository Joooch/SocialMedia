export type PaginatedResult<T> = {
    offset?: Date;
    page?: number;
    pageSize: number;
    total: number;
    items: T[];
}