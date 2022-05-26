import './styles/index.css';
import { BrowserRouter, useRoutes } from 'react-router-dom';
import HomePage from 'pages/Home';
import LoginPage from 'pages/Login';
import ProfilePage from 'pages/Profile';
import { NavigationTopBar } from 'features/navigation/ui/bars/top';
import { useAuth } from 'shared/api/session';
import ProfileSetupPage from 'pages/ProfileSetup';
import { CircularProgress, Container } from '@mui/material';
import { Box } from '@mui/system';

const Routes = () => {
  let routes = useRoutes([
    { path: "/", element: <HomePage /> },
    { path: "/settings", element: <ProfileSetupPage /> },
    { path: "/profile/:id", element: <ProfilePage /> }
  ])
  return routes;
}

function PageLayout() {
  return (
    <div className="center-content">
      <Container maxWidth="md">
        <Routes />
      </Container>
    </div>
  )
}

function LoggedPage(props: { veryfied: boolean }) {
  if (props.veryfied) {
    return (
      <>
        <NavigationTopBar />
        <PageLayout />
      </>
    )
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
      <BrowserRouter>
        {!logged
          ? <LoginPage />
          : <LoggedPage veryfied={!!user} />
        }
      </BrowserRouter>
    )
  }
}

export default AppWrapper;
