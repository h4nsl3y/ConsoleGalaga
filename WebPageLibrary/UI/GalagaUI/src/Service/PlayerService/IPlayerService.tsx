import type { IGridData } from "src/Model/GridData/IGridData";
import type { IPlayer } from "@/Model/Player/IPlayer";
import type { IPagination } from "@/Model/Pagination/IPagination";

export interface IPlayerService {
    addPlayer(playerName: string): Promise<number>;
    getPlayer(playerId: number): Promise<IPlayer>;
    deletePlayer(playerId: number): Promise<boolean>;
    editPlayer(playerId: number, playerName: string): Promise<boolean>;
    getAllPlayers({ page, limit }: IPagination): Promise<IGridData<IPlayer>>;
}
