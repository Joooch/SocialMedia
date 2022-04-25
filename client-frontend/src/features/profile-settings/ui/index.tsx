import TextField from '@mui/material/TextField';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { MobileDatePicker } from '@mui/x-date-pickers/MobileDatePicker';
import { useMemo, useState } from "react";
import { Box, Button, Grid, Stack } from '@mui/material';
import React from 'react';
import { UserAvatar } from 'entities/user';

type FormErrors = {
    firstName?: boolean,
    lastName?: boolean,
    address?: boolean,
    region?: boolean,
    city?: boolean,
    zipCode?: boolean,
    county?: boolean,
}
type ValidateProps = {
    date?: Date | null,
    firstName?: string,
    lastName?: string,
    address?: string,
    region?: string,
    city?: string,
    county?: string,
}

const isStringInvalid = (value: string | undefined, minLength: number, maxLength: number): boolean | undefined => {
    if (!value) {
        return undefined
    }

    const len = value.length
    return len < minLength || len > maxLength
}

const validateForm = (props: ValidateProps, setErrors?: (errors: FormErrors) => void): boolean => {
    const errors: FormErrors = {
        firstName: isStringInvalid(props.firstName, 5, 50),
        lastName: isStringInvalid(props.lastName, 5, 50),
        address: isStringInvalid(props.address, 5, 100),
        city: isStringInvalid(props.city, 5, 30),
        region: isStringInvalid(props.region, 2, 50),
        county: isStringInvalid(props.county, 2, 20),
    }
    if (setErrors) {
        setErrors(errors);
    }

    // check if there any error
    for (const error of Object.values(errors)) {
        if (error === true || error === undefined) {
            return false;
        }
    }
    return true;
}

export default function ProfileSettings(props: { profileImage?: string }) {
    let _profileImage = props.profileImage ?? "/img/user.png"

    const maxDate = new Date();
    maxDate.setFullYear(maxDate.getFullYear() - 15);

    const [date, setDate] = useState<Date | null>(maxDate);
    const [firstName, setFirstName] = useState<string>("");
    const [lastName, setLastName] = useState<string>("");
    const [address, setAddress] = useState<string>("");
    const [city, setCity] = useState<string>("");
    const [region, setRegion] = useState<string>("");
    const [county, setCounty] = useState<string>("");
    const [profileImage, setProfileImage] = useState<string>(_profileImage);


    const [errors, setErrors] = useState<FormErrors>();
    const fileInput = React.useRef<HTMLInputElement>(null);

    useMemo(() => {
        const isValid = validateForm({ date, firstName, lastName, address, city, region: region, county }, setErrors)
        console.log({ isValid })
    }, [date, firstName, lastName, address, city, region, county])

    const doUploadFile = () => {
        console.log({ fileInput })
        fileInput?.current?.click();
    }
    const onSelectImage = (event: React.ChangeEvent<HTMLInputElement>) => {
        console.log("on image change")
        const files = event.target.files;
        if (!files) {
            console.log("! files")
            return;
        }
        const image = files[0];
        setProfileImage(URL.createObjectURL(image))
        console.log(profileImage)
    }

    const upload = () => {
        let isImageChangd = _profileImage !== profileImage;
        
    }

    return (
        <Box className="profile-settings center-content">
            <Grid container spacing={3}>
                <Grid item xs className='center-content center-text'>
                    <Stack>
                        <UserAvatar src={profileImage} size={128} />
                        <Button onClick={doUploadFile}>
                            Change
                        </Button>
                        <input
                            type="file"
                            ref={fileInput}
                            onChange={onSelectImage}
                            style={{ display: 'none' }}
                        />
                        {/* <img className='.avatar-image' src={profileImage}/> */}
                        {/* {_profileImage == profileImage ? "" : "Click save to update"} */}
                    </Stack>
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        id="email"
                        name="email"
                        label="Email"
                        fullWidth
                        autoComplete="email"
                        variant="outlined"
                        disabled={true}
                        value={"Helo"}
                    />
                </Grid>
                <Grid item xs={12} sm={6}>
                    <TextField
                        required
                        id="firstName"
                        name="firstName"
                        label="First name"
                        fullWidth
                        autoComplete="given-name"
                        variant="outlined"
                        value={firstName}
                        error={errors?.firstName}
                        onChange={x => setFirstName(x.target.value)}
                    />
                </Grid>
                <Grid item xs={12} sm={6}>
                    <TextField
                        required
                        id="lastName"
                        name="lastName"
                        label="Last name"
                        fullWidth
                        autoComplete="family-name"
                        variant="outlined"
                        value={lastName}
                        error={errors?.lastName}
                        onChange={x => setLastName(x.target.value)}
                    />
                </Grid>
                <Grid item xs={12}>
                    <LocalizationProvider dateAdapter={AdapterDateFns}>
                        <MobileDatePicker
                            label="Birthday"
                            inputFormat="MM/dd/yyyy"
                            value={date}
                            onChange={setDate}
                            renderInput={(params) => <TextField fullWidth {...params} />}
                            maxDate={maxDate}
                        />
                    </LocalizationProvider>
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        required
                        id="address"
                        name="address"
                        label="Address"
                        fullWidth
                        autoComplete="shipping address-line1"
                        variant="outlined"
                        value={address}
                        error={errors?.address}
                        onChange={x => setAddress(x.target.value)}
                    />
                </Grid>
                <Grid item xs={12} sm={6}>
                    <TextField
                        required
                        id="city"
                        name="city"
                        label="City"
                        fullWidth
                        autoComplete="shipping address-level2"
                        variant="outlined"
                        value={city}
                        error={errors?.city}
                        onChange={x => setCity(x.target.value)}
                    />
                </Grid>
                <Grid item xs={12} sm={6}>
                    <TextField
                        required
                        id="state"
                        name="state"
                        label="State/Province/Region"
                        fullWidth
                        variant="outlined"
                        value={region}
                        error={errors?.region}
                        onChange={x => setRegion(x.target.value)}
                    />
                </Grid>
                <Grid item xs={12} sm={6}>
                    <TextField
                        required
                        id="country"
                        name="country"
                        label="Country"
                        fullWidth
                        autoComplete="shipping country"
                        variant="outlined"
                        value={county}
                        error={errors?.county}
                        onChange={x => setCounty(x.target.value)}
                    />
                </Grid>
            </Grid>
        </Box>
    )
}