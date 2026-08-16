import type { IAppUserService } from "@/Service/AppUserService/IAppUserService";
import type { IApiResponse } from "@/Model/ApiResponse/IApiResponse";
import type { IGridData } from "@/Model/GridData/IGridData";
import type { IAppUser } from "@/Model/AppUser/IAppUser";
import baseAPI from "@/API/BaseApi";


export class AppUserService implements IAppUserService {

    authenticate = async (username: string, password: string): Promise<boolean> => {
        const response = await baseAPI.post<IApiResponse<boolean>>(`/api/AppUser/authenticate`, { username, password });
        return response.data.data;
    }

    register = async (username: string, password: string): Promise<boolean> => {
        const response = await baseAPI.post<IApiResponse<boolean>>(`/api/AppUser/register`, { username, password });
        return response.data.data;
    }

    getAllUsers = async (page = 0, limit = 10): Promise<IGridData<IAppUser>> => {
        const response = await baseAPI.get<IApiResponse<IGridData<IAppUser>>>(`/api/AppUser/all`, { params: { page, limit } });
        return response.data.data;
    }

    deleteUser = async (userId: number): Promise<boolean> => {
        // Controller uses both route template and query binding for userId.
        const response = await baseAPI.delete<IApiResponse<boolean>>(`/api/AppUser/${userId}`, { params: { userId } });
        return response.data.data;
    }

    updateUser = async (username: string, password: string): Promise<boolean> => {
        const response = await baseAPI.put<IApiResponse<boolean>>(`/api/AppUser`, { username, password });
        return response.data.data;
    }
}
