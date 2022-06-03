import {
    Container,
    Stack,
} from '@mui/material'
import { useSelector } from 'react-redux';
import CreateDiscountDialog from './CreateDiscountDialog';
import { selectDiscountIds } from './discountsSlice';
import DiscountsTable from './DiscountsTable';

const DiscountsPage = () => {
    const discountIds = useSelector(selectDiscountIds);

    return (
        <>
            <Container maxWidth="lg">
                <Stack alignItems='center' gap={2}>
                    <DiscountsTable discountIds={discountIds}/>
                    <CreateDiscountDialog />
                </Stack>
            </Container>
        </>
    )
}

export default DiscountsPage;