const express = require('express');
const router = express.Router();
const FizzBuzzController = require('../controllers/fizzbuzz-controller'); 

router.get('/', (req, res, next) => new FizzBuzzController().get(req, res, next));
router.delete('/', (req, res, next) => new FizzBuzzController().delete(req, res, next));

module.exports = router;