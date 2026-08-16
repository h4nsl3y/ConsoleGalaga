import type { IScoreRecord } from "@/Model/ScoreRecord/IScoreRecord";
import type { IPagination } from "@/Model/Pagination/IPagination";

export interface IScoreRecordService {
    addScoreRecord(playerId: number, score: number): Promise<boolean>;
    getScoreRecord(scoreRecordId: number): Promise<IScoreRecord>;
    getTopScoreRecords({ page, limit }: IPagination): Promise<IScoreRecord[]>;
    deleteScoreRecord(scoreRecordId: number): Promise<boolean>;
    editScoreRecord(scoreRecordId: number, playerId: number, score: number): Promise<boolean>;
}
