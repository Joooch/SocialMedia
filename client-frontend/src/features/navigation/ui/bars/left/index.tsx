import Button from '@mui/material/Button'
import Typography from '@mui/material/Typography';
import { Send } from '@mui/icons-material';
import './index.css';


const settings = ['Profile', 'Account', 'Logout'];

interface StringMap { [key: string]: string; }
const pages: StringMap = {
    "Products": "/login",
}

export function LeftNavigationBar() {
    return (
        <div className="nav-bar-left">
            <Button
                variant="text"
                color="primary"
                startIcon={<Send />}
                fullWidth

            >
                h1. Heading
            </Button>
        </div>
    );
};