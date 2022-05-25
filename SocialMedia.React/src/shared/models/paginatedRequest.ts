export class Filter {
    path: string;
    value: string;

    constructor(path: string, value: string) {
        this.path = path;
        this.value = value;
    }
}

export type PaginatedRequest = {
    offset?: Date;
    page?: number;
    pageSize: number;
    filters?: Filter[];

    sortKey?: string;
    sortDirection?: string;
}