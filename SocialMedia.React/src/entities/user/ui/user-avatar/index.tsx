import { Skeleton } from "@mui/material"
import { useCallback, useMemo, useState } from "react"
import { Link } from "react-router-dom"
import { User } from "shared/models"
import "./user-avatar.css"

const defaultUserImg = "/img/user.png"

export const UserAvatar = (props: {
	user?: User | string
	img?: string
	size?: number | string
	supressLink?: boolean
}) => {
	const [imageSrc, setImageSrc] = useState<string>()

	useMemo(() => {
		if (props.img) {
			setImageSrc(props.img || defaultUserImg)
		} else if (typeof props.user === "string") {
			setImageSrc("/img/users/" + props.user + ".webp")
		} else {
			setImageSrc(
				"/img/users/" + props.user?.userId + ".webp" || defaultUserImg
			)
		}
	}, [props.user])

	const ImgComponent = useCallback(() => {
		return (
			<img
				className="user-avatar"
				alt={
					!props.user || typeof props.user == "string"
						? "user avatar"
						: props.user?.firstName ?? "user-avatar"
				}
				src={imageSrc}
				width={props.size}
				height={props.size}
				onError={() => {
					if (imageSrc !== defaultUserImg) {
						setImageSrc(defaultUserImg)
					}
				}}
			/>
		)
	}, [imageSrc, props.size, props.user])

	if (props.user === undefined) {
		// render skeleton instead
		return (
			<div className="user-avatar">
				<Skeleton
					className="user-avatar"
					variant="circular"
					width={props.size}
					height={props.size}
				/>
			</div>
		)
	}

	let link: string

	const user = props.user as User
	if (user) {
		link = "/profile/" + (typeof user === "string" ? user : user.userId)
	} else {
		link = ""
	}

	if (props.supressLink) {
		return <ImgComponent />
	} else {
		return (
			<Link to={link}>
				{" "}
				<ImgComponent />{" "}
			</Link>
		)
	}
}
