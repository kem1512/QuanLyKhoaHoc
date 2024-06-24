"use client";

import { Grid, Group, SimpleGrid, Stack, TextInput } from "@mantine/core";
import {
  IPracticeMapping,
  ITestCaseMapping,
  TestCaseClient,
  TestCaseCreate,
  TestCaseUpdate,
} from "../../app/web-api-client";
import { handleSubmit } from "../../lib/helper";
import { useEffect, useState } from "react";
import useSWR from "swr";
import ActionButton, {
  ActionButtonIcon,
} from "../../components/Helper/ActionButton";
import { IconCheck, IconTrash } from "@tabler/icons-react";

export default function TestCaseHandler({
  practice,
}: {
  practice?: IPracticeMapping;
}) {
  const TestCaseService = new TestCaseClient();

  const { data, mutate } = useSWR(`/api/testCase/${practice.id}`, () =>
    TestCaseService.getEntities(practice.id, null, null, null, null)
  );

  const [testCases, setTestCases] = useState<ITestCaseMapping[]>([]);
  const [newTestCase, setNewTestCase] = useState<ITestCaseMapping>({
    input: "",
    output: "",
    practiceId: practice.id,
    programingLanguageId: practice.languageProgrammingId,
  });

  useEffect(() => {
    if (data?.items) {
      setTestCases(data.items);
    }
  }, [data]);

  const handleChange = (index: number, key: string, value: string) => {
    const updatedTestCases = [...testCases];
    updatedTestCases[index] = { ...updatedTestCases[index], [key]: value };
    setTestCases(updatedTestCases);
  };

  const handleSave = (index: number) => {
    const testCase = testCases[index];
    handleSubmit(
      () => {
        return testCase.id
          ? TestCaseService.updateEntity(
              testCase.id,
              testCase as TestCaseUpdate
            )
          : TestCaseService.createEntity(testCase as TestCaseCreate);
      },
      `${testCase.id ? "Sửa" : "Thêm"} Thành Công`,
      mutate
    );
  };

  const handleNewChange = (key: string, value: string) => {
    setNewTestCase((prev) => ({ ...prev, [key]: value }));
  };

  const handleNewSave = () => {
    handleSubmit(
      () => TestCaseService.createEntity(newTestCase as TestCaseCreate),
      "Thêm Thành Công",
      mutate
    ).then(() => {
      setNewTestCase({
        input: "",
        output: "",
        practiceId: practice.id,
        programingLanguageId: practice.languageProgrammingId,
      });
    });
  };

  if (practice.id == null) return null;

  return (
    <>
      {testCases.map((item, index) => (
        <Grid.Col span={6} key={item.id || index}>
          <SimpleGrid cols={3}>
            <TextInput
              label="Input"
              w={"100%"}
              placeholder="Nhập Input"
              value={item.input}
              onChange={(e) => handleChange(index, "input", e.target.value)}
              labelProps={{ style: { marginBottom: 6 } }}
            />
            <TextInput
              label="Output"
              w={"100%"}
              placeholder="Nhập Output"
              value={item.output}
              onChange={(e) => handleChange(index, "output", e.target.value)}
              labelProps={{ style: { marginBottom: 6 } }}
            />
            <Group mt="auto" mb="6">
              <ActionButtonIcon
                variant="light"
                action={() => handleSave(index)}
              >
                <IconCheck />
              </ActionButtonIcon>
              <ActionButtonIcon
                variant="light"
                color="red"
                action={() =>
                  handleSubmit(
                    () => TestCaseService.deleteEntity(item.id),
                    "Xóa Thành Công",
                    mutate
                  )
                }
              >
                <IconTrash />
              </ActionButtonIcon>
            </Group>
          </SimpleGrid>
        </Grid.Col>
      ))}

      <Grid.Col span={6}>
        <SimpleGrid cols={3}>
          <TextInput
            label="Input"
            placeholder="Nhập Input"
            value={newTestCase.input}
            onChange={(e) => handleNewChange("input", e.target.value)}
            labelProps={{ style: { marginBottom: 6 } }}
          />
          <TextInput
            label="Output"
            placeholder="Nhập Output"
            value={newTestCase.output}
            onChange={(e) => handleNewChange("output", e.target.value)}
            labelProps={{ style: { marginBottom: 6 } }}
          />
          <ActionButton
            size="xs"
            action={handleNewSave}
            mt="auto"
            mb="3"
            me="auto"
          >
            Thêm Mới
          </ActionButton>
        </SimpleGrid>
      </Grid.Col>
    </>
  );
}
