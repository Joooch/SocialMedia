import React from 'react';
import ReactDOM from 'react-dom';
import App from './app';
import './index.css';
import * as serviceWorker from './serviceWorker';

import { AuthProvider } from "shared/api/session";

import CssBaseline from "@mui/material/CssBaseline";
import { ThemeProvider } from '@mui/material/styles';
import createTheme from "@mui/material/styles/createTheme";

const darkTheme = createTheme({
  palette: {
    mode: 'dark',
    primary: {
      main: '#90caf9',
    },
    secondary: {
      main: '#f48fb1',
    },
    background: {
      default: '#212121',
      paper: '#424242',
    },
  },
});

ReactDOM.render(
  <React.StrictMode>
    <ThemeProvider theme={darkTheme}>
      <CssBaseline />
      <AuthProvider>
        <App />
      </AuthProvider>
    </ThemeProvider>
  </React.StrictMode>,
  document.getElementById('root')
);

serviceWorker.unregister();
