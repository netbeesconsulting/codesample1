export class PaginationModel {
    id: number;
    skip: number;
    take: number;
    currentPage: number;
    totalRecords: number;

    constructor(take) {
        this.skip = 0;
        this.take = take;
        this.totalRecords = 0;
        this.currentPage = 1;
    }
}