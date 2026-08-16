import baseAPI from "@/API/BaseApi";
import type { IApiResponse } from "@/Model/ApiResponse/IApiResponse";
import type { IPlayer } from "@/Model/Player/IPlayer";
import type { IPagination } from "@/Model/Pagination/IPagination";
import type { IGridData } from "src/Model/GridData/IGridData";
import type { IPlayerService } from "@/Service/PlayerService/IPlayerService";

export class PlayerService implements IPlayerService {
    addPlayer = async (playerName: string): Promise<number> => {
        // Controller expects a raw string body: [FromBody] string playerName.
        const response = await baseAPI.post<IApiResponse<number>>("/api/Player", JSON.stringify(playerName));

        return response.data.data;
    };

    getPlayer = async (playerId: number): Promise<IPlayer> => {
        const response = await baseAPI.get<IApiResponse<IPlayer>>(`/api/Player`, {
            params: { playerId },
        });
        return response.data.data;
    };

    deletePlayer = async (playerId: number): Promise<boolean> => {
        // Controller uses route template and query binding for playerId.
        const response = await baseAPI.delete<IApiResponse<boolean>>(`/api/Player/${playerId}`, {
            params: { playerId },
        });
        return response.data.data;
    };

    editPlayer = async (playerId: number, playerName: string): Promise<boolean> => {
        const response = await baseAPI.put<IApiResponse<boolean>>("/api/Player", {
            playerId,
            playerName,
        });

        return response.data.data;
    };

    getAllPlayers = async ({ page = 0, limit = 10 }: IPagination): Promise<IGridData<IPlayer>> => {
        const response = await baseAPI.get<IApiResponse<IGridData<IPlayer>>>("/api/Player/all", {
            params: { page, limit },
        });

        return response.data.data;
    };
}
