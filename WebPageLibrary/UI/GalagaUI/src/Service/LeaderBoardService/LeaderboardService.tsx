import type { ILeaderBoardService } from "@/Service/LeaderBoardService/ILeaderboardService";
import type { IPagination } from "@/Model/Pagination/IPagination";
import type { IApiResponse } from "@/Model/ApiResponse/IApiResponse";
import baseAPI from "@/API/BaseApi";
import type { IRecord } from "src/Model/Record/IRecord";
import type { IGridData } from "src/Model/GridData/IGridData";


export class LeaderBoardService implements ILeaderBoardService {

    getLeaderboardRecords = async ({page = 0, limit = 10 } : IPagination): Promise<IGridData<IRecord>> => {
        const response = await baseAPI.get<IApiResponse<IGridData<IRecord>>>(`/api/LeaderBoard/top`, {
            params: { page, limit },
        });

        if (!response.data.success) {
            console.log(response.data.message || "Failed to fetch leaderboard records");
        }   
        console.log("API Response:", response.data.data);
        return response.data.data;
    }
}