import { useParams } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { useLocation } from "react-router-dom";

export default function NotFound() {
    const navigate = useNavigate();
    const location = useLocation();

    function goBack() {
        console.log("PATH: "+location.pathname);
        navigate("/");
    }
}