export const dateToStringFormatter = (date: Date) => {
    const year = date.getFullYear()
    const month = String(date.getMonth() + 1).padStart(2, '0')
    const day = String(date.getDate()).padStart(2, '0')
    return `${year}-${month}-${day}`
}

export const stringToDateFormatter = (date: string) => {
    const parts = date.split('-')
    if (parts[0] && parts[1] && parts[2]) {
        const year = parseInt(parts[0], 10)
        const month = parseInt(parts[1], 10)
        const day = parseInt(parts[2], 10)

        return new Date(year, month - 1, day)
    }

    return new Date()
}
