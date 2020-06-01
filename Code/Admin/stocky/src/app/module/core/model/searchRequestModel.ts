export class SearchRequestModel {
    skip: number;
    take: number;
    searchTerm: string;
    orderBy: string;
    totalRecords: number;
    currentPage: number;

    constructor(take) {
        this.skip = 0;
        this.take = take;
        this.searchTerm = "";
        this.orderBy = "asc";
    }
}