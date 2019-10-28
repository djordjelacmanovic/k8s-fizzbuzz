process.env.FIZZBUZZ_REDIS_COUNTER_KEY = 'test-redis-key'

const FizzBuzzController = require('../../controllers/fizzbuzz-controller');
const FizzBuzzer = require('../../services/fizzbuzzer');

const mockGetFizzBuzzedString = jest.fn().mockReturnValue('TestFizzBuzzResult');
jest.mock('../../services/fizzbuzzer', () => jest.fn().mockImplementation(() => {
  return {
    getFizzBuzzedString: mockGetFizzBuzzedString
  };
}));

const mockIncr = jest.fn().mockResolvedValue(15);
const mockDel = jest.fn().mockResolvedValue('OK');
jest.mock('../../libs/redis', () => {
  return {
    incr: mockIncr,
    del: mockDel
  };
});

class MockResponse {

  get status(){
    return this.status;
  }

  status(status){
    this.status = status;
    return this;
  }

  sendStatus(status){
    return this.status(status);
  }
  
  json(data){
    this.data = data;
    return this;
  }
};

describe(FizzBuzzController, () => {

  beforeEach(() => {
    FizzBuzzer.mockClear();
    mockGetFizzBuzzedString.mockClear();
    mockDel.mockClear();
    mockIncr.mockClear();
  });

  describe('.ctor()', () => {
    it('instantiates the FizzBuzzer service', () => {
      new FizzBuzzController();
      expect(FizzBuzzer).toHaveBeenCalledTimes(1);
    });
  });

  describe('#get()', () => {
    const _ = {};

    it('increments the key in redis', async () => {
      await new FizzBuzzController().get(_, new MockResponse());
      expect(mockIncr).toHaveBeenCalledWith(process.env.FIZZBUZZ_REDIS_COUNTER_KEY);
    });

    it('calls the FizzBuzzer with value from redis INCR', async () => {
      await new FizzBuzzController().get(_, new MockResponse());
      expect(mockGetFizzBuzzedString).toHaveBeenCalledWith(15);
    });

    it('returns the correct JSON result', async () => {
      const controller = new FizzBuzzController();
      const mockResponse = new MockResponse();
      await controller.get(_, mockResponse);
      expect(mockResponse.data).toEqual({
        value: 'TestFizzBuzzResult',
        app_name: process.env.npm_package_name,
        app_version: process.env.npm_package_version
      });
    });
  });

  describe('#delete()', () => {

    it('sends status code 200', async () => {
      const mockRes = new MockResponse();
      await new FizzBuzzController().delete({}, mockRes);
      expect(mockRes.status).toBe(200);
    });
    
    it('resets the redis counter', async () => {
      await new FizzBuzzController().delete({}, new MockResponse());
      expect(mockDel).toHaveBeenCalledWith(process.env.FIZZBUZZ_REDIS_COUNTER_KEY);
    });
  });
});