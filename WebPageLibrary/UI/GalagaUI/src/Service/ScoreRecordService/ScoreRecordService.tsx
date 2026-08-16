import baseAPI from "@/API/BaseApi";
import type { IApiResponse } from "@/Model/ApiResponse/IApiResponse";
import type { IPagination } from "@/Model/Pagination/IPagination";
import type { IScoreRecord } from "@/Model/ScoreRecord/IScoreRecord";
import type { IScoreRecordService } from "@/Service/ScoreRecordService/IScoreRecordService";

export class ScoreRecordService implements IScoreRecordService {
    addScoreRecord = async (playerId: number, score: number): Promise<boolean> => {
        const response = await baseAPI.post<IApiResponse<boolean>>("/api/ScoreRecord", {
            playerId,
            score,
        });
        return response.data.data;
    };

    getScoreRecord = async (scoreRecordId: number): Promise<IScoreRecord> => {
        const response = await baseAPI.get<IApiResponse<IScoreRecord>>(`/api/ScoreRecord`, {
            params: { scoreRecordId },
        });
        return response.data.data;
    };

    getTopScoreRecords = async ({ page = 0, limit = 10 }: IPagination): Promise<IScoreRecord[]> => {
        const response = await baseAPI.get<IApiResponse<IScoreRecord[]>>(`/api/ScoreRecord/top`, {
            params: { page, limit },
        });
        return response.data.data;
    };

    deleteScoreRecord = async (scoreRecordId: number): Promise<boolean> => {
        const response = await baseAPI.delete<IApiResponse<boolean>>(`/api/ScoreRecord`, {
            params: { scoreRecordId },
        });
        return response.data.data;
    };

    editScoreRecord = async (scoreRecordId: number, playerId: number, score: number): Promise<boolean> => {
        const response = await baseAPI.put<IApiResponse<boolean>>("/api/ScoreRecord", {
            scoreRecordId,
            playerId,
            score,
        });
        return response.data.data;
    };
}
