import UserIcon from "@/assets/UserIcon"

const Navbar = ({ onUserClick }: { onUserClick: () => void }) => {
    return (
        <div className="w-full h-1/10 flex items-center justify-center flex-col">
            <div className="w-9/10 h-9/10 m-2 glass rounded">
                <div className="h-full w-full flex">
                    <div className="flex-1">

                    </div>
                    <div className="flex items-center justify-center flex-4 text-center">
                        <h1>Welcome to Galaga!</h1>
                    </div>
                    <div className="flex items-center justify-center flex-1 cursor-pointer" onClick={onUserClick}>
                        <UserIcon className="h-6/10 aspect-square icon-base"/>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Navbar