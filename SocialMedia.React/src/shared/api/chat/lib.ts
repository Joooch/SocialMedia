import { PaginatedResult, User } from "shared/models"
import * as api from "../apiRequest"

export interface IChatMessage {
	id: string
	ownerId: string
	content: string
}

type eventCallback = (data: any) => void
export class ChatEvents {
	private readonly _triggers: { [key: string]: eventCallback[] } = {}
	public on(event: string, callback: eventCallback): number {
		if (!this._triggers[event]) {
			this._triggers[event] = []
		}

		return this._triggers[event].push(callback)
	}

	public triggerHandler(event: string, data: any) {
		if (this._triggers[event]) {
			for (const i in this._triggers[event]) {
				this._triggers[event][i](data)
			}
		}
	}

	public removeEventListener(event: string, id: number) {
		this._triggers[event]?.splice(id, 1)
	}
}

const lowercaseKeys = (object: any) => {
	Object.keys(object).forEach((key) => {
		const value = object[key]

		delete object[key]
		object[key.charAt(0).toLowerCase() + key.slice(1)] = value

		if (typeof value === "object") {
			lowercaseKeys(value)
		}
	})
}

interface ICommand {
	type: string
	payload: any
}

export class Chat {
	private _events: ChatEvents
	private _socket: WebSocket | null = null
	public _connected: boolean = false

	constructor() {
		this._events = new ChatEvents()
	}

	/**
	 * login
	 */
	public async login(token: string): Promise<boolean> {
		this.close()

		return new Promise((res, rej) => {
			this._connected = false

			this._socket = new WebSocket(
				`ws://${window.location.host}/api/chat/?token=` + token
			)

			const self = this
			this._socket.onopen = function () {
				self._connected = true
				res(true)

				/* this.send(
					JSON.stringify({
						Type: "SendMessage",
						Payload: {
							TargetId: "cbf8e21f-4f17-43af-7ad0-08db5cab8373",
							Message: "Hello world",
						},
					})
				) */
				this.onclose = () => {}
				this.onclose = self.onClose
			}
			this._socket.onmessage = (e) => {
				const command = JSON.parse(e.data) as ICommand
				if (!command) {
					self.onMessage(e.data)
					return
				}
				lowercaseKeys(command)

				if (command.type) {
					command.type = command.type.toLowerCase()
				}
				self.onMessage(command)
			}
			this._socket.onclose = (e) => {
				rej(e.reason)
			}
		})
	}

	private send(payload: object) {
		if (!this._socket) {
			return
		}

		this._socket.send(JSON.stringify(payload))
	}

	public sendMessage(targetId: string, message: string) {
		this.send({
			Type: "SendMessage",
			Payload: {
				TargetId: targetId,
				Message: message,
			},
		})
	}

	private onMessage(data: ICommand) {
		if (!data?.type) {
			return
		}
		if (process.env.NODE_ENV === "development") {
			console.log({ data })
		}
		this._events.triggerHandler(data.type, data.payload)
	}
	public onClose() {
		this._connected = false
	}

	public on(event: string, callback: eventCallback): number {
		return this._events.on(event, callback)
	}

	public removeEventListener(event: string, id: number) {
		this._events.removeEventListener(event, id)
	}

	public close() {
		this._socket?.close()
	}
}

export async function getFriendChats(targetId: string) {
    let response = await api.post<User>("/api/chat/friendMessages", { targetId });
    return response.data;
}

export async function getLastMessages(targetId: string) {
	const response = await api.get<PaginatedResult<IChatMessage>>(
		`/api/chat/messages/${targetId}`,
		{
			params: {
				pageSize: 50,
				SortKey: "CreatedAt",
				SortDirection: "DESC",
			},
		}
	)
	return response.data
}
