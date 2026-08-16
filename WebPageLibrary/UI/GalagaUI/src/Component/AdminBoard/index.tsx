// import LoginForm from "../LoginForm";
import AdminControlBoard from "@/Component/AdminControlBoard";

const AdminBoard = () => {
    return(
        <div className={`h-full w-full flex items-center justify-center flex-col text-center`}>
            <div className="h-1/10 w-full">
                <h1>Admin Board</h1>
            </div>
            <div className="h-9/10 w-full">
                {/* <LoginForm/> */}
                <AdminControlBoard/>
            </div>
            
        </div>
    )
}

export default AdminBoard;