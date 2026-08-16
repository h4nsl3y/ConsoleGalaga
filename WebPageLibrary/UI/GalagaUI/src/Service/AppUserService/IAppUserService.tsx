import type { IGridData } from "@/Model/GridData/IGridData";
import type { IAppUser } from "@/Model/AppUser/IAppUser";

export interface IAppUserService {
    authenticate(username: string, password: string): Promise<boolean>;
    register(username: string, password: string): Promise<boolean>;
    getAllUsers(page?: number, limit?: number): Promise<IGridData<IAppUser>>;
    deleteUser(userId: number): Promise<boolean>;
    updateUser(username: string, password: string): Promise<boolean>;
}
