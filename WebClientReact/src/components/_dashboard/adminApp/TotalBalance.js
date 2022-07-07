// material
import {styled} from '@mui/material/styles';
import {Card, Typography, Stack} from '@mui/material';
import React from "react";
import roundAccountBalance from '@iconify/icons-ic/round-account-balance';
import {Icon} from "@iconify/react";
// ----------------------------------------------------------------------

const RootStyle = styled(Card)(({theme}) => ({
    boxShadow: 9,
    textAlign: 'left',
    paddingLeft: 30,
    paddingTop: 20,
    color: '#915c5c',
    backgroundColor: '#f4e3e3',
    width: 325,
    height: 154,
}));

const IconWrapperStyle = styled('div')(({ theme }) => ({
    margin: 'auto',
    display: 'flex',
    borderRadius: '50%',
    marginRight: -17,
    paddingTop: 0,
    alignItems: 'center',
    width: theme.spacing(16),
    height: theme.spacing(20),
    justifyContent: 'center',
    color: '#915c5c'
}));

// ----------------------------------------------------------------------

export default function TotalBalance() {
    return (

        <RootStyle>
            <Stack flexDirection='row'>
                <Stack flexDirection='column'>
                    <Typography variant="subtitle1" paddingLeft={0} paddingTop={1}>
                        Total Balance
                    </Typography>
                    <Typography variant="h3" sx={{paddingTop: 3}}>
                        677
                    </Typography>
                </Stack>
                <IconWrapperStyle>
                    <Icon icon={roundAccountBalance} width={350} height={350}/>
                </IconWrapperStyle>
            </Stack>
        </RootStyle>
    );
}
