import { useState } from "react";
import UserIcon from "@/assets/UserIcon";
import { AppUserService } from "@/Service/AppUserService/AppUserService";

const appUserService = new AppUserService();

const LoginForm = () => {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");
    const [loading, setLoading] = useState(false);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setError("");
        setLoading(true);

        try {
            const success = await appUserService.authenticate(username, password);
            if (!success) {
                setError("Invalid username or password.");
            }
        } catch {
            setError("Authentication failed. Please try again.");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="w-full h-full flex items-center justify-center">
            <form onSubmit={handleSubmit} className="w-full max-w-md glass rounded p-8 flex flex-col items-center gap-6">
                <div className="w-20 h-20 rounded-full flex items-center justify-center">
                    <UserIcon className="h-full icon-base" />
                </div>

                <h2 className="text-2xl font-bold tracking-wide">Sign In</h2>
                <p className="text-sm opacity-70 -mt-4">Enter your credentials to continue</p>

                <div className="w-full flex flex-col gap-1">
                    <label htmlFor="username" className="text-sm text-(--text) opacity-80 pl-1">
                        Username
                    </label>
                    <input
                        id="username"
                        type="text"
                        role="textbox"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        placeholder="Enter your username"
                        className="w-full px-4 py-3 rounded input "/>
                </div>

                <div className="w-full flex flex-col gap-1">
                    <label htmlFor="password" className="text-sm text-(--text) opacity-80 pl-1">
                        Password
                    </label>
                    <input
                        id="password"
                        type="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        placeholder="Enter your password"
                        className="w-full px-4 py-3 rounded input "/>
                </div>

                <button type="submit" disabled={loading} className="w-full py-3 mt-2 rounded text-(--text) cursor-pointer glass disabled:opacity-50 disabled:cursor-not-allowed">
                    {loading ? "Authenticating..." : "Launch"}
                </button>

                {error && <p className="text-red-400 text-sm -mt-2">{error}</p>}
            </form>
        </div>
    );
};

export default LoginForm;