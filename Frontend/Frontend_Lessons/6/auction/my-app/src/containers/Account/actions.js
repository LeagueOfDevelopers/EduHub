export const GET_AVALIABLE_LOTS = 'GET_AVALIABLE_LOTS';

export const PLACE_BET = 'PLACE_BET';

export const getAvaliableLots = () => ({type: GET_AVALIABLE_LOTS})

export const placeBet = (lotId, amount) => ({type: PLACE_BET, payload: {lotId, amount}})