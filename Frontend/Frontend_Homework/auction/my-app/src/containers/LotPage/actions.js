export const PLACE_BET = 'PLACE_BET';

export const placeBet = (lotId, amount) => {
    return {
        type: PLACE_BET,
        payload: {lotId, amount}
    }
}