import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import { LocalizationProvider, MobileDatePicker } from '@mui/x-date-pickers';
import React, { useMemo, useState } from "react";
import { Box, Button, Grid, Stack } from '@mui/material';
import { UserAvatar } from 'entities/user';
import SendIcon from '@mui/icons-material/Send';
import { profileUpdate, ProfileUpdateDto, profileUpdateImage } from 'shared/api/profile'
import { CircularProgress, TextField } from '@mui/material';
import { UserFull } from 'shared/api';

type FormErrors = {
    firstName?: boolean,
    lastName?: boolean,
    address?: boolean,
    region?: boolean,
    city?: boolean,
    zipCode?: boolean,
    county?: boolean,

    [x: string]: boolean | undefined
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

const validateForm = (props: ValidateProps, setErrors?: (errors: FormErrors) => void, assertNull?: boolean): boolean => {
    const errors: FormErrors = {
        firstName: isStringInvalid(props.firstName, 3, 50),
        lastName: isStringInvalid(props.lastName, 3, 50),
        address: isStringInvalid(props.address, 5, 100),
        city: isStringInvalid(props.city, 5, 30),
        region: isStringInvalid(props.region, 2, 50),
        county: isStringInvalid(props.county, 2, 20),
    }
    if (assertNull) {
        for (const key in errors) {
            const error = errors[key];
            if (error === null || error === undefined) {
                errors[key] = true
            }
        }
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

export default function ProfileSettings(props: { profile?: UserFull, email: string, onChange?: () => void }) {

    const maxDate = new Date();
    maxDate.setFullYear(maxDate.getFullYear() - 15);

    const [date, setDate] = useState<Date | null>(maxDate);
    const [firstName, setFirstName] = useState<string>(props.profile?.firstName ?? "");
    const [lastName, setLastName] = useState<string>(props.profile?.lastName ?? "");
    const [address, setAddress] = useState<string>(props.profile?.address ?? "");
    const [city, setCity] = useState<string>(props.profile?.city ?? "");
    const [region, setRegion] = useState<string>(props.profile?.region ?? "");
    const [country, setCountry] = useState<string>(props.profile?.country ?? "");
    const [profileImage, setProfileImage] = useState<string | undefined>(props.profile ? "/img/users/" + props.profile.userId + ".webp" : undefined);

    const [uploading, setUploading] = useState<boolean>(false);

    const [errors, setErrors] = useState<FormErrors>();
    const fileInput = React.useRef<HTMLInputElement>(null);

    useMemo(() => {
        validateForm({ date, firstName, lastName, address, city, region: region, county: country }, setErrors)
    }, [date, firstName, lastName, address, city, region, country])

    const doUploadFile = () => {
        fileInput?.current?.click();
    }
    const onSelectImage = (event: React.ChangeEvent<HTMLInputElement>) => {
        const files = event.target.files;
        if (!files || files.length === 0) {
            return;
        }

        const image = files[0];
        //setProfileImage(URL.createObjectURL(image));

        profileUpdateImage(image).then(res => {
            setProfileImage(res.data.filename + "?" + new Date().getTime())
        })
    }

    const uploadChanges = () => {
        if (date == null) {
            return
        }
        const isValid = validateForm({ date, firstName, lastName, address, city, region: region, county: country }, setErrors, true);
        if (!isValid) {
            return;
        }

        const dto: ProfileUpdateDto = {
            firstName: firstName, lastName, address, city, region, country, date
        }

        setUploading(true)
        profileUpdate(dto).then(e => {
            console.log(e)
            console.log(e.data)
        }).finally(() => {
            setTimeout(() => {
                setUploading(false)
                if(props.onChange){
                    props?.onChange()
                }
            }, 1000);
        })
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
                        value={props.email}
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
                <Grid item xs={12} sm={6}>
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
                        id="country"
                        name="country"
                        label="Country"
                        fullWidth
                        autoComplete="shipping country"
                        variant="outlined"
                        value={country}
                        error={errors?.county}
                        onChange={x => setCountry(x.target.value)}
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
                <Grid item xs={12} className="center-content">
                    <Button variant="contained" color="success" size="large" startIcon={<SendIcon />} onClick={() => uploadChanges()}>
                        Submit
                    </Button>
                    {uploading ? <CircularProgress sx={{ ml: 24, position: "absolute" }} /> : undefined}
                </Grid>
            </Grid>
        </Box >
    )
}