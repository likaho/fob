pragma solidity ^0.5.11;

/**
 * @title Storage
 * @dev Store & retreive value in a variable
 */
contract ProsperStorage {

    struct state {
        uint32 submissionDate;
        string hash;
    }
    mapping(uint256 => state) map;
    uint256 length;

    /**
     * @dev Store value in variable
     */
    function store(uint32 id, uint32 submissionDate, string calldata hash) external {
		uint256 hashKey = id*1000000+submissionDate;
        map[hashKey].submissionDate = submissionDate;
        map[hashKey].hash = hash;
        length++;
    }

    /**
     * @dev Return value 
     */
    function retreive(uint32 id, uint32 submissionDate) external view returns (string memory){
		uint256 hashKey = id*1000000+submissionDate;
        return map[hashKey].hash;
    }
    
    function getCount() external view returns (uint256) {
        return length;
    }
}