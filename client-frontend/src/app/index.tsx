import './styles/index.css';
import { BrowserRouter, useRoutes } from 'react-router-dom';
import HomePage from 'pages/Home';
import LoginPage from 'pages/Login';
import ProfilePage from 'pages/Profile';
import Grid from '@mui/material/Grid';
import { NavigationTopBar } from 'features/navigation/ui/bars/top';
import { LeftNavigationBar } from 'features/navigation/ui/bars/left';
import { useAuth } from 'shared/api/session';
import ProfileSetupPage from 'pages/ProfileSetup';
import { CircularProgress } from '@mui/material';
import { Box } from '@mui/system';

const Routes = () => {
  let routes = useRoutes([
    { path: "/", element: <HomePage /> },
    { path: "/settings", element: <ProfileSetupPage /> },
    { path: "/profile/:id", element: <ProfilePage /> }
    //{ path: "/login", element: <Login /> },
  ])
  return routes;
}

function PageLayout() {
  return <div className="center-content">
    <Grid container wrap='nowrap' justifyContent="space-evenly" xl={9}>
      <Grid item whiteSpace="nowrap" xs sx={{ mr: 4, display: { xs: 'none', md: 'none', lg: "block" } }}>
        <LeftNavigationBar />
      </Grid>

      <Grid item xs={9}>
        <Routes />
      </Grid>

    </Grid>
  </div>
}

function LoggedPage(props: { veryfied: boolean }) {
  if (props.veryfied) {
    return <BrowserRouter> <NavigationTopBar /> <PageLayout /> </BrowserRouter>
  } else {
    return <ProfileSetupPage />
  }
}

function AppWrapper() {
  const { user, loading, logged } = useAuth()

  if (loading) {
    return (
      <Box className="center-of-screen">
        <CircularProgress size={"100px"} />
      </Box>
    )
  } else {
    return (
      <>
        {!logged
          ? <LoginPage />
          : <LoggedPage veryfied={!!user} />
        }
      </>
    )
  }
}

export default AppWrapper;
