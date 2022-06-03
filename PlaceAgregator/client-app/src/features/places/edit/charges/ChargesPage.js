import {
    Container,
    Stack,
} from '@mui/material'
import ChargesTable from './ChargesTable'
import CreateChargeDialog from './CreateChargeDialog'
import { useSelector } from 'react-redux';
import { selectChargeIds } from './chargesSlice';

const ChargesPage = () => {
    const chargeIds = useSelector(selectChargeIds);
    return (
        <>
            <Container maxWidth="lg">
                <Stack alignItems='center' gap={2}>
                    <ChargesTable chargeIds={chargeIds}/>
                    <CreateChargeDialog />
                </Stack>
            </Container>
        </>
    )
}

export default ChargesPage;