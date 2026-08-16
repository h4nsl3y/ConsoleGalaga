import LeaderBoard from "@/Component/LeaderBoard"
import AdminBoard from "@/Component/AdminBoard"

const Area = ({ showAdmin }: { showAdmin: boolean }) => {
    return(
        <div className={`w-full h-9/10 flex items-center justify-center flex-col`}>
            <div className={`w-9/10 h-9/10 flex items-center justify-center glass rounded`}>
                <div className={`w-9/10 h-9/10 flex flex-col items-center justify-center`}>
                    {showAdmin ? <AdminBoard/> : <LeaderBoard/>}
                </div>
            </div>
        </div>
    )
}

export default Area