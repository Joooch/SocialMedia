import {
	Divider,
	List,
	ListItemAvatar,
	ListItemButton,
	ListItemText
} from "@mui/material"
import { UserAvatar } from "entities/user"
import { Fragment, useEffect, useState } from "react"
import { useAuth } from "shared/api"
import { getFriends } from "shared/api/friends"
import { User } from "shared/models"
import "./index.css"

export default function ChatFriendsList({
	selectedChat,
	onFriendSelected,
}: {
	selectedChat?: string
	onFriendSelected?: (id: string) => void
}) {
	const { user } = useAuth()
	const [friends, setFriends] = useState<User[]>([])

	// let response = await api.post<PaginatedResult<User>>("/api/friends/getFriends", { userId, pageInfo });
	useEffect(() => {
		getFriends(user!.userId, {
			pageSize: 100,
		}).then((_friends) => {
			setFriends(_friends.items)
		})
	}, [])

	return (
		<div className="chat-friends">
			<List
				sx={{
					width: 360,
					bgcolor: "background.default",
				}}
			>
				{friends.map((friend) => (
					<ListItemButton
						alignItems="flex-start"
						key={friend.userId}
						sx={{
							bgcolor:
								"background." +
								(selectedChat === friend.userId
									? "paper"
									: "default"),
						}}
						onClick={() =>
							onFriendSelected && onFriendSelected(friend.userId)
						}
					>
						<ListItemAvatar>
							<UserAvatar user={friend} size={40} />
						</ListItemAvatar>
						<ListItemText
							primary={`${friend.firstName} ${friend.lastName}`}
							secondary={
								<Fragment>Check incoming message</Fragment>
							}
						/>
					</ListItemButton>
				))}
				<Divider variant="inset" component="li" />
			</List>
		</div>
	)
}
