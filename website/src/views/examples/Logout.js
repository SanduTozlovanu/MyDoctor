import { useEffect } from "react"
import { useHistory } from "react-router-dom";

const Logout = () => {
    const history = useHistory()

    useEffect(() => {
        localStorage.clear()
        return history.push("/auth/login")
    }, [history])

    return null;

}

export default Logout;