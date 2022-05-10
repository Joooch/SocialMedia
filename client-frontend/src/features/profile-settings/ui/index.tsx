import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import { LocalizationProvider, MobileDatePicker } from '@mui/x-date-pickers';
import React, { useEffect, useState } from "react";
import { Box, Button, Grid, Stack } from '@mui/material';
import { UserAvatar } from 'entities/user';
import SendIcon from '@mui/icons-material/Send';
import { profileUpdate, ProfileUpdateDto, profileUpdateImage } from 'shared/api/profile'
import { CircularProgress, TextField } from '@mui/material';
import { UserFull } from 'shared/api';
import { Controller, useForm } from 'react-hook-form';

/* type ProfileUpdateDto = {
    date?: Date | null,
    firstName?: string,
    lastName?: string,
    address?: string,
    region?: string,
    city?: string,
    country?: string,
} */
const maxDate = new Date()
maxDate.setFullYear(maxDate.getFullYear() - 15);

export default function ProfileSettings(props: { profile?: UserFull, email: string, onChange?: () => void }) {
    const defaultValue = (() => {
        if (!props.profile) {
            return {}
        }

        return {
            "date": maxDate,
            "firstName": props.profile.firstName,
            "lastName": props.profile.lastName,
            "region": props.profile.region,
            "city": props.profile.city,
            "country": props.profile.country,
            "address": props.profile.address
        }
    })()

    const { control, handleSubmit, formState: { errors } } = useForm<ProfileUpdateDto>({
        reValidateMode: "onBlur",
        defaultValues: defaultValue
    });


    const [profileImage, setProfileImage] = useState<string | undefined>(props.profile ? "/img/users/" + props.profile.userId + ".webp" : undefined);
    const [uploading, setUploading] = useState<boolean>(false);

    const fileInput = React.useRef<HTMLInputElement>(null);

    useEffect(() => {
        if (props.profile === null) {
            setProfileImage("")
        }
    }, [props.profile])

    const doUploadFile = () => {
        fileInput?.current?.click();
    }
    const onSelectImage = (event: React.ChangeEvent<HTMLInputElement>) => {
        const files = event.target.files;
        if (!files || files.length === 0) {
            return;
        }

        const image = files[0];

        profileUpdateImage(image).then(res => {
            setProfileImage(res.data.filePath + "?" + new Date().getTime())
        })
    }

    const onSubmit = (form: ProfileUpdateDto) => {
        setUploading(true)
        profileUpdate(form).then(e => {

        }).finally(() => {
            setTimeout(() => {
                setUploading(false)
                if (props.onChange) {
                    props.onChange()
                }
            }, 1000);
        })
    }

    return (
        <Box className="profile-settings center-content">
            <Grid container spacing={3}>
                <Grid item xs className='center-content center-text'>
                    <Stack>
                        <UserAvatar user={props.profile ?? profileImage} size={128} />
                        <Button onClick={doUploadFile}>
                            Change
                        </Button>
                        <input
                            type="file"
                            ref={fileInput}
                            onChange={onSelectImage}
                            title="imageSelector"
                            style={{ display: 'none' }}
                        />
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
                    <Controller
                        control={control}
                        name="firstName"
                        rules={{
                            "required": true,
                        }}
                        render={
                            ({ field }) => <TextField
                                {...field}
                                error={errors.firstName !== undefined}
                                required
                                label="First name"
                                fullWidth
                                autoComplete="given-name"
                                variant="outlined"
                            />
                        }
                    />
                </Grid>
                <Grid item xs={12} sm={6}>
                    <Controller
                        control={control}
                        name="lastName"
                        rules={{
                            "required": true,
                        }}
                        render={
                            ({ field }) => <TextField
                                {...field}
                                error={errors.lastName !== undefined}
                                required
                                label="Last name"
                                fullWidth
                                autoComplete="family-name"
                                variant="outlined"
                            />
                        }
                    />
                </Grid>
                <Grid item xs={12}>
                    <LocalizationProvider dateAdapter={AdapterDateFns}>
                        <Controller
                            control={control}
                            name="date"
                            rules={{
                                "required": true,
                            }}
                            render={
                                ({ field }) => <MobileDatePicker
                                    {...field}
                                    label="Birthday"
                                    inputFormat="MM/dd/yyyy"
                                    renderInput={(params) => <TextField error={errors.date !== undefined} fullWidth {...params} />}
                                    maxDate={maxDate}
                                />
                            }
                        />
                    </LocalizationProvider>
                </Grid>
                <Grid item xs={12} sm={6}>
                    <Controller
                        control={control}
                        name="address"
                        rules={{
                            "required": true,
                        }}
                        render={
                            ({ field }) => <TextField
                                {...field}
                                required
                                label="Address"
                                fullWidth
                                autoComplete="shipping address-line1"
                                variant="outlined"
                            />
                        }
                    />
                </Grid>
                <Grid item xs={12} sm={6}>
                    <Controller
                        control={control}
                        name="country"
                        rules={{
                            "required": true,
                        }}
                        render={
                            ({ field }) => <TextField
                                {...field}
                                error={errors.country !== undefined}
                                required
                                label="Country"
                                fullWidth
                                autoComplete="shipping country"
                                variant="outlined"
                            />
                        }
                    />
                </Grid>
                <Grid item xs={12} sm={6}>
                    <Controller
                        control={control}
                        name="city"
                        rules={{
                            "required": true,
                        }}
                        render={
                            ({ field }) => <TextField
                                {...field}
                                error={errors.city !== undefined}
                                required
                                label="City"
                                fullWidth
                                autoComplete="shipping address-level2"
                                variant="outlined"
                            />
                        }
                    />
                </Grid>
                <Grid item xs={12} sm={6}>
                    <Controller
                        control={control}
                        name="region"
                        rules={{
                            "required": true,
                        }}
                        render={
                            ({ field }) => <TextField
                                {...field}
                                error={errors.region !== undefined}
                                required
                                label="State/Province/Region"
                                fullWidth
                                variant="outlined"
                            />
                        }
                    />
                </Grid>
                <Grid item xs={12} className="center-content">
                    <Button variant="contained" color="success" size="large" startIcon={<SendIcon />} onClick={handleSubmit(onSubmit)}>
                        Submit
                    </Button>
                    {uploading ? <CircularProgress sx={{ ml: 24, position: "absolute" }} /> : undefined}
                </Grid>
            </Grid>
        </Box >
    )
}