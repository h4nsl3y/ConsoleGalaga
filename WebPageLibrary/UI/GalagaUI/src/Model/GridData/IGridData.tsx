export interface IGridData<T> {
    titles: string[];
    records: T[];
    currentPage: number;
    totalPages: number;
    totalRecords: number;
}