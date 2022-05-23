import { AppBar, Button, Toolbar, Typography } from "@mui/material"

const AppHeader = (props) => {
    const { onClose } = props;

    return (
        <>
            <AppBar color='secondary'>
                <Toolbar>
                    <Typography>
                        Панель персонала
                    </Typography>
                    <Button
                        onClick={onClose}
                        sx={{ ml: 'auto' }}
                        variant='text'
                        color='inherit'
                    >
                        Выйти
                    </Button>
                </Toolbar>
            </AppBar>
            <Toolbar></Toolbar>
        </>
    )
}

export default AppHeader;